class CreateActivities < ActiveRecord::Migration[8.1]
  def change
    create_table :activities do |t|
      t.references :business, null: false, foreign_key: true
      t.string :name
      t.text :description
      t.boolean :is_active, default: true

      t.timestamps
    end
  end
end
