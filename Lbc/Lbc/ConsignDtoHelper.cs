using Lbc.Models;
using Lbc.WebApi.Modes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.LBC.Consign.WebApi.Dtos;

namespace Lbc {
    public static class ConsignDtoHelper {

        public static IEnumerable<ConsignDetail> Convert(this ConsignDto dto) {
            if (dto == null)
                return null;

            var lst = new List<ConsignDetail>() {
                new ConsignDetail("", "委托人", dto.ConsignConsigner.ConsignerNameCn ),
                new ConsignDetail("", "联系人", dto.ConsignConsigner.ConsignContacts !=null && dto.ConsignConsigner.ConsignContacts.Count() > 0 ? string.Format("{0} {1}", dto.ConsignConsigner.ConsignContacts.First().Fullname, dto.ConsignConsigner.ConsignContacts.First().Mobile) : "" ),
                new ConsignDetail("", "柜型柜量", dto.ConsignContainerGoods.GetContaDesc()),
                new ConsignDetail("", "货物", string.Join(";", dto.ConsignContainerGoods.GetGoodsDescs())),
                new ConsignDetail("", "船东", string.Join(";", dto.ConsignCarrier.CarrierName)),
                new ConsignDetail("费用资料", "应收", ""),
                new ConsignDetail("费用资料", "应付", ""),
                new ConsignDetail("费用资料", "参考利率", ""),
                new ConsignDetail("SO提单", "订舱号", ""),
                new ConsignDetail("SO提单", "提单类型", ""),
                new ConsignDetail("业务资料", "贸易条款", ""),
                new ConsignDetail("业务资料", "运输条款", ""),
            };

            return lst;
        }

        public static ListViewGroupedModel<ConsignDetail> ToListViewGroupData(this ConsignDto dto) {
            var lst = dto.Convert();
            var a = lst.ToLookup(l => l.Group)
                .Select(l => new ListViewGroup<ConsignDetail>(l) {
                    Title = l.Key,
                    ShortTitle = l.Key,

                });

            return new ListViewGroupedModel<ConsignDetail>() {
                Groups = new ObservableCollection<ListViewGroup<ConsignDetail>>(a)
            };
        }

        public static string GetContaDesc(this ConsignContainerGoodsDto dto) {
            var dic = new Dictionary<string, int?>() {
                    {"20GP", dto.Gp20},
                    {"40GP", dto.Gp40},
                    {"40HQ", dto.Hq40},
                    {"45HQ", dto.Hq45},
                };

            return string.Join(";", dic.Where(kv => kv.Value.HasValue && kv.Value > 0)
                .Select(kv => string.Format("{0}x{1}", kv.Value, kv.Key)));
        }

        public static IEnumerable<string> GetGoodsDescs(this ConsignContainerGoodsDto dto) {
            return dto.Goodses.Select(g => string.Format("{0} ({1} {2})", g.GoodsDesc, g.Qty, g.QtyUnit));
        }
    }
}
